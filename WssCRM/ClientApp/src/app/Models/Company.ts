import {Stage} from './Stage'
import { Manager } from './Manager';
export class Company {

  constructor(
    public name?: string,
    public managers?: Manager[],
    public stages?: Stage[],
    public id ? : number
    
  ) {
      this.managers = [];
      this.stages = [];
    }
}
