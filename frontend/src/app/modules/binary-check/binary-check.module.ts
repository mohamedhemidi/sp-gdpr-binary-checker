import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BinaryCheckRoutingModule } from './binary-check-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckStringComponent } from './pages/check-string/check-string.component';
import { EntriesListComponent } from './components/entries-list/entries-list.component';
import { EntryCardComponent } from './components/entry-card/entry-card.component';

@NgModule({
  declarations: [
    CheckStringComponent,
    EntriesListComponent,
    EntryCardComponent,
  ],
  imports: [CommonModule, ReactiveFormsModule, BinaryCheckRoutingModule],
  exports: [EntriesListComponent],
})
export class BinaryCheckModule {}
