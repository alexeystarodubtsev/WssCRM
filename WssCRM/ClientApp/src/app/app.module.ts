import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, ErrorStateMatcher, ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';  
import { MatFormFieldModule } from '@angular/material/form-field';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { RolesComponent } from './ManageRoles/roles.component';
import { UsersRolesComponent } from './ManageRoles/usersRoles.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CallsComponent } from './calls/calls.component'
import { CompaniesComponent } from './companies/companies.component'
import { CallComponent } from './call/call.component'
import { missedClientComponent } from './missedClients/missedClient.component'
import { StageComponent } from './stage/stage.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbdModalContent } from './ModalWindow/ModalWindowComponent'
import { NgbPaginationModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NewCallComponent } from './NewCall/NewCallComponent'
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { statisticsComponent } from './statistics/statisticsComponent';
import { AuthanticationComponent } from './Authentication/Authantication.component';
import { AlertComponent } from './_components/alert.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CallsComponent,
    CompaniesComponent,
    CallComponent,
    StageComponent,
    NgbdModalContent,
    NewCallComponent,
    missedClientComponent,
    statisticsComponent,
    AlertComponent,
    AuthanticationComponent,
    UsersRolesComponent,
    RolesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatSelectModule,
    MatTableModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    MatFormFieldModule,
    NgbPaginationModule,
    NgbAlertModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatRadioModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule,
    AppRoutingModule,
    
  ],
  providers: [MatDatepickerModule,
    MatNativeDateModule,
    MatNativeDateModule,
    MatInputModule,
    MatIconModule,
    { provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
