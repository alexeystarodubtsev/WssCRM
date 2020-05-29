import { Point } from './Point'
import { Company } from './Company';
import { Stage } from './Stage';
import { Manager } from './Manager';
import { FormControl } from '@angular/forms';
export class Call {
  constructor(
    public id?: number,
    public stage?: Stage,
    public points?: Point[],
    public company?: Company,
    public date?: FormControl,
    public quality?: number,
    public manager?: Manager
  ) {
    this.company = new Company();
    this.points = [];
  }
  
}
