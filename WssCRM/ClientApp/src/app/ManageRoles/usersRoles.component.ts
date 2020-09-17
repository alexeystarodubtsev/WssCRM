import { Component, OnInit } from '@angular/core';
import { UserWithRoles } from '../Models/userWithRoles';
import { User } from '../Models/User';
import { DataService } from '../_services/';
import { error } from 'protractor';
import { AlertService } from '../_services';
@Component({
  selector: 'app-usersroles',
  templateUrl: './usersRoles.component.html',
  providers: [DataService, AlertService]
})
export class UsersRolesComponent implements OnInit {

  curuser: UserWithRoles;
  users: User[];
  constructor(private dataservice: DataService, private alertService: AlertService) {
  }
  ngOnInit() {
    this.load(); 
  }
  load() {
    this.dataservice.getUsers().subscribe((data: User[]) => {
      this.users = data;
    },
      error => {
        this.alertService.error(JSON.stringify(error));
      });
  }
  getUsersRoles(user: User) {
    this.curuser = undefined;
    this.dataservice.getUserRoles(user.id).subscribe((data: UserWithRoles) => {
      this.curuser = data;
      
    },
      error => {
        this.alertService.error(JSON.stringify(error));
      });
  }
  
  
}
