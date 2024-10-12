import { Component, Input } from '@angular/core';
import moment from 'moment';
@Component({
  selector: 'app-entry-card',
  templateUrl: './entry-card.component.html',
  styleUrl: './entry-card.component.scss',
})
export class EntryCardComponent {
  @Input() binaryString: string = '';
  @Input() checkedDate: string = '';
  @Input() good: boolean = false;

  moment: any = moment;
}
