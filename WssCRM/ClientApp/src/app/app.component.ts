import { Component } from '@angular/core';
import { environment } from './../environments/environment';
import { User } from './Models/User';
import { DataService as AccountService } from './_services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [AccountService]
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
