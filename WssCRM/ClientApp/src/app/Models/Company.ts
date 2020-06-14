import {Stage} from './Stage'
import { Manager } from './Manager';
export class Company {

  constructor(
    public name?: string,
    public managers?: Manager[],
    public stages?: Stage[],
    public daysForAnalyze ? : number,
    public id ? : number
    
  ) {
      this.managers = [];
    this.stages = [];
    this.daysForAnalyze = 21;
    }
}
