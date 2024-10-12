import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CheckStringComponent } from './pages/check-string/check-string.component';

const routes: Routes = [{ path: 'check', component: CheckStringComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BinaryCheckRoutingModule {}
