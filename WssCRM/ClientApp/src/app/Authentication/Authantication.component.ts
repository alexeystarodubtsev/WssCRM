import { Component } from '@angular/core';

import { DataService } from '../_services/';
import { User } from '../Models/User';

@Component({
  selector: 'app-authantication', templateUrl: 'authantication.component.html',
  providers: [DataService]  })
export class AuthanticationComponent {
  user: User;

  constructor(private accountService: DataService) {
    this.accountService.user.subscribe(x => this.user = x);
  }

  logout() {
    this.accountService.logout();
  }
}
