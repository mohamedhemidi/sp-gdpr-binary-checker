import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { EntriesService } from '../../services/entries.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-check-string',
  templateUrl: './check-string.component.html',
  styleUrl: './check-string.component.scss',
})
export class CheckStringComponent implements OnInit, OnDestroy {
  form: FormGroup;
  entriesService = inject(EntriesService);
  checkStringFormSubscription!: Subscription;
  error: string = '';

  /**
   *
   */
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
  ) {
    this.form = this.fb.group({
      binary_string: ['', Validators.required],
    });
  }

  onSubmit() {
    this.checkStringFormSubscription = this.entriesService
      .checkEntry({
        BinaryString: this.form.value.binary_string,
      })
      .subscribe({
        next: (response) => {
          console.log(response);
          this.router.navigateByUrl('/dashboard');
        },
        error: (err) => {
          this.error = err.error.Message;
        },
      });
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    if (this.checkStringFormSubscription) {
      this.checkStringFormSubscription.unsubscribe();
    }
  }
}
