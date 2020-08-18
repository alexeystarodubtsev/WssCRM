export class TableWithStatistic {

  constructor(
    public  tableName? : string,
    public period?: string[],
    public managers?: string[],
    public data? : RowInTableStatistic[],
    public mode? :string      
        

  ) {
    this.period = [];
    this.managers = [];
    this.data = [];
  }
}
class CellInStatistic {
  constructor(
    public period?: string,
    public value?: string

  ){ }
}
class RowInTableStatistic {
  constructor(
  public rowname ? : string,
    public cells?: CellInStatistic[]
  )
  {
    cells = [];
  }
}
