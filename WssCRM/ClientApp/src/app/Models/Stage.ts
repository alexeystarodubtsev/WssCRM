import {Point} from './Point'
export class Stage {

  constructor(
    public name?: string,
    public points?: Point[],
    public id?: number

  ) {
    this.points = [];
  }
}
