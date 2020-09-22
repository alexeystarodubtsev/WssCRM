import { Component, OnInit } from '@angular/core';
import { DataService, AlertService } from '../_services';
import { Role } from '../Models/Role';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  providers: [DataService]
})
export class RolesComponent implements OnInit {
  constructor(private dataService: DataService, private alertService: AlertService) { }

  roles: Role[]
  loading: boolean = true;
  ngOnInit() {
    this.loadRoles();     
  }
  loadRoles() {
    this.dataService.getRoles()
      .subscribe((data : Role[]) => {
        this.roles = data;
        this.loading = false;
      }, (error) => {
        this.alertService.error(JSON.stringify(error));
        this.loading = false;
      }
   )
  }
}
