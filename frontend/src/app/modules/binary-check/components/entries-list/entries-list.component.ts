import { Component, inject, OnInit } from '@angular/core';
import { EntriesService } from '../../services/entries.service';
import { IResponse } from '../../../../shared/types/IResponse';
import { IEntry } from '../../types/entries';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-entries-list',
  templateUrl: './entries-list.component.html',
  styleUrl: './entries-list.component.scss',
})
export class EntriesListComponent implements OnInit {
  entriesService = inject(EntriesService);
  entriesList$!: Observable<IResponse<IEntry[]>>;

  ngOnInit(): void {
    this.getEntriesList();
  }

  getEntriesList() {
    this.entriesList$ = this.entriesService.getEntries();
  }

  onDelete(entryId: string) {
    this.entriesService
      .deleteEntry({
        Id: entryId,
      })
      .subscribe({
        next: (response) => {
          this.getEntriesList();
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
}
