import { isPlatformBrowser } from '@angular/common';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private tokenSubject = new BehaviorSubject<string | null>(null);

  constructor(
    private router: Router
  ) { }

  logout(): void {
    localStorage.removeItem('token');
    this.tokenSubject.next(null);
    this.router.navigate(['/']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    console.log(this.getToken());
    return !!this.getToken();
  }
}
