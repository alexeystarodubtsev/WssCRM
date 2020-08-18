"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var TableWithStatistic = /** @class */ (function () {
    function TableWithStatistic(tableName, period, managers, data, mode) {
        this.tableName = tableName;
        this.period = period;
        this.managers = managers;
        this.data = data;
        this.mode = mode;
        this.period = [];
        this.managers = [];
        this.data = [];
    }
    return TableWithStatistic;
}());
exports.TableWithStatistic = TableWithStatistic;
var CellInStatistic = /** @class */ (function () {
    function CellInStatistic(period, value) {
        this.period = period;
        this.value = value;
    }
    return CellInStatistic;
}());
var RowInTableStatistic = /** @class */ (function () {
    function RowInTableStatistic(rowname, cells) {
        this.rowname = rowname;
        this.cells = cells;
        cells = [];
    }
    return RowInTableStatistic;
}());
//# sourceMappingURL=TableWithStatistic.js.map