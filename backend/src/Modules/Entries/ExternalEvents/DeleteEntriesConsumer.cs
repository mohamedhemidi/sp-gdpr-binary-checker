using Common.Contracts.Messaging.Events;
using MassTransit;
using MongoDB.Driver;
using Entries.Entities;

namespace Entries.ExternalEvents
{
    public class DeleteEntriesConsumer : IConsumer<DeleteAllEntriesEvent>
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<Entry> _entriesCollection;

        public DeleteEntriesConsumer(IMongoClient mongoClient, IMongoDatabase mongoDatabase)
        {
            _mongoClient = mongoClient;
            _entriesCollection = mongoDatabase.GetCollection<Entry>("entries");
        }
        public async Task Consume(ConsumeContext<DeleteAllEntriesEvent> context)
        {
            // Delete of All Entries related to the deleted User
            using (var session = await _mongoClient.StartSessionAsync())
            {
                session.StartTransaction();

                try
                {
                    var filter = Builders<Entry>.Filter.Eq(p => p.UserId, context.Message.UserId);
                    var entriesList = await _entriesCollection.Find(filter).ToListAsync();

                    DeleteResult result = await _entriesCollection.DeleteManyAsync(session, filter);

                    if (entriesList.Any())
                    {
                        if (result.DeletedCount > 0)
                        {
                            await context.RespondAsync(new CommandSuccess(true));

                            await session.CommitTransactionAsync();

                        }
                        await session.AbortTransactionAsync();
                        await context.RespondAsync(new CommandSuccess(false));


                    }

                    await context.RespondAsync(new CommandSuccess(true));

                    await session.CommitTransactionAsync();

                }
                catch (System.Exception)

                {
                    await context.RespondAsync(new CommandSuccess(false));

                    throw;
                }
            }

        }
    }
}
