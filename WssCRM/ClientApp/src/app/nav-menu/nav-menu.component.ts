import { Component, Output, EventEmitter, Input } from '@angular/core';
import { User } from '../Models/User';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;
  @Output() childToParent = new EventEmitter;
  @Input() user: User;
  collapse() {
    this.isExpanded = true;
    console.log(window.innerWidth);
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  logout() {
    this.childToParent.emit();
  }
}
