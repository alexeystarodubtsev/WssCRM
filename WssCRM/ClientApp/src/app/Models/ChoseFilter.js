"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Company_1 = require("./Company");
var ChoseFilter = /** @class */ (function () {
    function ChoseFilter(company, stage, manager, StartDate, //Date,
    EndDate, //Date
    pageNumber, onlyNotProcessed, period) {
        this.company = company;
        this.stage = stage;
        this.manager = manager;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
        this.pageNumber = pageNumber;
        this.onlyNotProcessed = onlyNotProcessed;
        this.period = period;
        this.company = new Company_1.Company();
        this.pageNumber = 1;
        this.onlyNotProcessed = true;
    }
    return ChoseFilter;
}());
exports.ChoseFilter = ChoseFilter;
//# sourceMappingURL=ChoseFilter.js.map