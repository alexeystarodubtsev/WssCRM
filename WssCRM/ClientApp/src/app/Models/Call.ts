import { Point } from './Point'
import { Company } from './Company';
import { Stage } from './Stage';
import { Manager } from './Manager';
import { FormControl } from '@angular/forms';
import { Time } from '@angular/common';
import { Objection } from './Objection';
export class Call {
  constructor(
    public id?: number,
    public stage?: Stage,
    public points?: Point[],
    public company?: Company,
    public date?: FormControl,
    public quality?: number,
    public manager?: Manager,
    public correction?: string,
    public correctioncolor? : string,
    public duration?: Time,
    public ClientName?: string,
    public ClientLink?: string,
    public clientState?: string,
    public Objections?: Objection[],
    public hasObjections?: boolean,
    public DateNext?: FormControl
  ) {
    this.company = new Company();
    this.points = [];
    this.Objections = [];
    this.stage = new Stage();
    this.manager = new Manager();
    
   
  }
  
}
