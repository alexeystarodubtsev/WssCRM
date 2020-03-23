import { Point } from './Point'
export class Call {
  constructor(
    public id? : number,
    public stage?: string,
    public points?: Point[],
    public company?: string,
    public date?: string,
    public TrueDate?: Date
    
  ) { }
}
