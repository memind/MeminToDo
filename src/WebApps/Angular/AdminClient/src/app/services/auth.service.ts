import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  token: String | null;
  constructor() {}

  receiveToken() {
    
  }

  isAuthenticated() {
    const token: string = localStorage.getItem("accessToken");

    if (token)
        _isAuthenticated = true;
    else
        _isAuthenticated = false;
  }

  getToken() {
    return this.token;
  }

  logout() {
    localStorage.removeItem("accessToken");
  }
}

export let _isAuthenticated: boolean;