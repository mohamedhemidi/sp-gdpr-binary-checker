import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckStringComponent } from './check-string.component';

describe('CheckStringComponent', () => {
  let component: CheckStringComponent;
  let fixture: ComponentFixture<CheckStringComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CheckStringComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CheckStringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
