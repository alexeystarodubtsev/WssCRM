import {Point} from './Point'
export class Stage {

  constructor(
    public name?: string,
    public points?: Point[],
    public id?: number,
    public deleted?: boolean,
    public agreementStage?: boolean,
    public preAgreementStage?: boolean,
    public incomeStage? : boolean

  ) {
    this.points = [];

    this.deleted = false;
    this.agreementStage = false;
    this.incomeStage = false;
    this.preAgreementStage = false;
  }
}
