import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { User } from '../_models/User';
import { Injectable } from '@angular/core';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import { Message } from '../_models/message';

@Injectable()

export class MessagesResolver implements Resolve<Message[]> {
    pageSize = 5;
    pageNumber = 1;
    messageContainer = 'Unread';

    constructor(
        private authService: AuthService,
        private userService: UserService,
        private router: Router,
        private alertify: AlertifyService
    ) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Message[]> {
        return this.userService.getMessages(
            this.authService.decodedToken.nameid,
            this.pageNumber,
            this.pageSize,
            this.messageContainer
            ).catch(error => {
                this.alertify.error('problem retrieving data');
                this.router.navigate(['/home']);
                return Observable.of(null);
        });
     }
}
