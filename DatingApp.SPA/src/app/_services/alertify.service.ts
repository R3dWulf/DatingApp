import { Injectable } from '@angular/core';
/*Make alertify available as a global variable inside the service*/
declare let alertify: any;

@Injectable()
export class AlertifyService {

    constructor() { }

    confirm(message: string, okCallback: () => any) {
        alertify.confirm(message, function(e) {
            if (e) {
                okCallback();
            } else {
                /*Do nothing for now*/
            }
        });
    }

    success(message: string) {
        alertify.success(message);
    }

    error(message: string) {
        alertify.error(message);
    }

    warning(message: string) {
        alertify.warning(message);
    }

    message(message: string) {
        alertify.message(message);
    }

}
