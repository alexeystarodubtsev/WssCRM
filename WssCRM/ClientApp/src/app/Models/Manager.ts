import { Point } from './Point'
export class Manager {
  constructor(
    public id?: number,
    public name?: string,
    public deleted?: boolean
  ) {
    this.deleted = false;
  }
}
