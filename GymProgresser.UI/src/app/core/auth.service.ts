// auth.service.ts
import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    constructor(private api: ApiService) { }

    login(credentials: { email: string; password: string }): Observable<void> {
        return this.api.post<{ accessToken: string }>('Auth/login', credentials).pipe(
            tap(response => {
                localStorage.setItem('accessToken', response.accessToken);
            }),
            map(() => void 0) 
        );
    }

    logout() {
        localStorage.removeItem('accessToken');
    }

    isAuthenticated(): boolean {
        return !!localStorage.getItem('accessToken');
    }
}
