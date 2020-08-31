import { Component } from '@angular/core';

import { AccountService } from './account.service';
import { User } from '../Models/User';

@Component({ selector: 'app-authantication', templateUrl: 'authantication.component.html' })
export class AuthanticationComponent {
  user: User;

  constructor(private accountService: AccountService) {
    this.accountService.user.subscribe(x => this.user = x);
  }

  logout() {
    this.accountService.logout();
  }
}
