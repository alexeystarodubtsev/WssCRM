import {Point} from './Point'
export class Stage {

  constructor(
    public name?: string,
    public points?: Point[],
    public id?: number,
    public deleted?: boolean

  ) {
    this.points = [];

    this.deleted = false;
  }
}
