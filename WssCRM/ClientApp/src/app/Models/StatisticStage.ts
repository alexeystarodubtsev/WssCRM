import {TableWithStatistic} from './TableWithStatistic'
export class StatisticStage {

  constructor(
    public stageName ? : string,
    public tables? : TableWithStatistic[]

  ) {
  }
}
