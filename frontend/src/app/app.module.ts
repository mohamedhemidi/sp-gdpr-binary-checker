import { AsyncPipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { BodyComponent } from './shared/components/body/body.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { accessTokenInterceptor } from './modules/authentication/interceptors/access-token.interceptor';
import { BinaryCheckModule } from './modules/binary-check/binary-check.module';
@NgModule({
  declarations: [
    AppComponent,
    BodyComponent,
    SidebarComponent,
    DashboardComponent,
    NavbarComponent,
  ],
  imports: [
    BrowserModule,
    AsyncPipe,
    BrowserAnimationsModule,
    AppRoutingModule,
    BinaryCheckModule,
  ],
  providers: [provideHttpClient(withInterceptors([accessTokenInterceptor]))],
  bootstrap: [AppComponent],
})
export class AppModule {}
