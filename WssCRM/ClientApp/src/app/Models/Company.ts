import {Stage} from './Stage'
export class Company {

  constructor(
    public name?: string,
    public managers?: string[],
    public stages?: Stage[],
    public id ? : number
    
  ) {
      this.managers = [];
      this.stages = [];
    }
}
