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
    public clientName?: string,
    public clientLink?: string,
    public clientState?: string,
    public objections?: Objection[],
    public hasObjections?: boolean,
    public dateNext?: FormControl,
    public hasDateNext?: boolean,
    public firstCalltoClient? : boolean

  ) {
    this.company = new Company();
    this.points = [];
    this.objections = [];
    this.stage = new Stage();
    this.manager = new Manager();
    this.hasDateNext = false;
    this.firstCalltoClient = false;
    
  }
  
}
