import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  constructor(
    private http: HttpClient
  ) { }

  url = environment.url;
  token: any = localStorage.getItem("token");

  getJobs() {
    const token: any = localStorage.getItem("token");
    const uri = this.url + `api/main/getJobs`;
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      }),
    };
    return this.http.get(uri, httpOptions);
  }



  getCountries() {
    const token: any = localStorage.getItem("token");
    const uri = this.url + `api/Main/getCountries`;
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      }),
    };
    return this.http.get(uri, httpOptions);
  }

  getProducts() {
    const token: any = localStorage.getItem("token");
    const uri = this.url + `api/Main/getProducts`;
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      }),
    };
    return this.http.get(uri, httpOptions);
  }

  getClinics() {
    const token: any = localStorage.getItem("token");
    const uri = this.url + `api/main/getClinics`;
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      }),
    };
    return this.http.get(uri, httpOptions);
  }

}
