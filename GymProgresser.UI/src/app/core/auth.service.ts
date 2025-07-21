// auth.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    constructor(private api: ApiService) { }
    private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.isAuthenticated());
    isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

    login(credentials: { email: string; password: string }): Observable<void> {
        return this.api.post<{ accessToken: string }>('Auth/login', credentials).pipe(
            tap(response => {
                localStorage.setItem('accessToken', response.accessToken);
                this.isAuthenticatedSubject.next(true);
            }),
            map(() => void 0)
        );
    }

    register(credentials: { email: string; password: string; confirmPassword: string }): Observable<void> {
        return this.api.post<void>('Auth/register', credentials);
    }

    logout() {
        localStorage.removeItem('accessToken');
    }

    isAuthenticated(): boolean {
        return !!localStorage.getItem('accessToken');
    }
}
