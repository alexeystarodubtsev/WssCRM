"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AuthGuard = /** @class */ (function () {
    function AuthGuard(router, accountService) {
        this.router = router;
        this.accountService = accountService;
    }
    AuthGuard.prototype.canActivate = function (route, state) {
        var user = this.accountService.userValue;
        if (user) {
            // authorised so return true
            return true;
        }
        // not logged in so redirect to login page with the return url
        this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url } });
        return false;
    };
    return AuthGuard;
}());
exports.AuthGuard = AuthGuard;
//# sourceMappingURL=auth.guard.js.map