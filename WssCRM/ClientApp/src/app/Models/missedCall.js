"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var missedCall = /** @class */ (function () {
    function missedCall(id, clientName, clientLink, clientState, date, reason, correction, noticeCRM, dateNext, manager, processed) {
        this.id = id;
        this.clientName = clientName;
        this.clientLink = clientLink;
        this.clientState = clientState;
        this.date = date;
        this.reason = reason;
        this.correction = correction;
        this.noticeCRM = noticeCRM;
        this.dateNext = dateNext;
        this.manager = manager;
        this.processed = processed;
        this.processed = false;
    }
    return missedCall;
}());
exports.missedCall = missedCall;
//# sourceMappingURL=missedCall.js.map