import { Component } from '@angular/core';
import { environment } from './../environments/environment';
import { User } from './Models/User';
import { AccountService } from './Authentication/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  user: User;
  constructor(private accountService: AccountService) {
    this.accountService.user.subscribe(x => this.user = x);
  }
  title = 'app';
  logout() {
    this.accountService.logout();
  }
}
