import { Component, OnInit, Input, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'ngbd-modal-content',
  templateUrl: './ModalWindowComponent.html'
    
  
})
export class NgbdModalContent {
  @Input() question;
  constructor(public activeModal: NgbActiveModal) { }
}


