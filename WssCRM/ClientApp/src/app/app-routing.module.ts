import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RolesComponent, UsersRolesComponent } from "./ManageRoles"
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_helpers';
import { statisticsComponent } from './statistics/statisticsComponent';
import { NewCallComponent } from './NewCall/NewCallComponent';
import { CallsComponent } from './calls/calls.component';
import { CompaniesComponent } from './companies/companies.component';
import { CallComponent } from './call/call.component';
import { missedClientComponent } from './missedClients/missedClient.component';


const accountModule = () => import('./account/account.module').then(x => x.AccountModule);


const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
 //, canActivate: [AuthGuard]
  { path: 'calls', component: CallsComponent, canActivate: [AuthGuard]},
  { path: 'companies', component: CompaniesComponent, canActivate: [AuthGuard] },
  { path: 'call/new', component: NewCallComponent, canActivate: [AuthGuard] },
  { path: 'missedcall', component: missedClientComponent, canActivate: [AuthGuard] },
  { path: 'call/:Id', component: CallComponent, canActivate: [AuthGuard]},
  { path: 'statistics', component: statisticsComponent, canActivate: [AuthGuard] },
  { path: 'users', component: UsersRolesComponent, canActivate: [AuthGuard] },
  { path: 'roles', component: RolesComponent, canActivate: [AuthGuard] },
  { path: 'account', loadChildren: accountModule },

  // otherwise redirect to home
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
