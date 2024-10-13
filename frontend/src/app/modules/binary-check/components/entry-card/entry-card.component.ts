import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import moment from 'moment';
import { EntriesService } from '../../services/entries.service';
import { Subscription } from 'rxjs';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-entry-card',
  templateUrl: './entry-card.component.html',
  styleUrl: './entry-card.component.scss',
  animations: [
    trigger('itemAnime', [
      transition('void => *', [
        style({
          height: 0,
          opacity: 0,
          transform: 'scale(0.8)',
          'margin-bottom': 0,
          paddingTop: 0,
          paddingBottom: 0,
          paddingLeft: 0,
          paddingRight: 0,
        }),
        animate(
          '50ms',
          style({
            height: '*',
            'margin-bottom': '*',
            paddingTop: '*',
            paddingBottom: '*',
            paddingLeft: '*',
            paddingRight: '*',
          }),
        ),
        animate(200),
      ]),
    ]),
  ],
})
export class EntryCardComponent {
  @Input() entryId: string = '';
  @Input() binaryString: string = '';
  @Input() checkedDate: string = '';
  @Input() good: boolean = false;

  @Output() deleteEntry = new EventEmitter<string>();

  entriesService = inject(EntriesService);
  deleteEntryFormSubscription!: Subscription;
  moment: any = moment;

  onDelete() {
    this.deleteEntry.emit(this.entryId);
  }
}
