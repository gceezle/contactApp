import { Injectable, Inject } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { Contact } from '../models/contact';

@Injectable()
export class ContactService {
    contacts: Contact[] = [];
    private _baseUrl: string;
    private headers: Headers;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this._baseUrl = baseUrl;
        this.headers = new Headers({ 'Content-Type': 'application/json' });
    }

    getAllContacts(): Observable<Contact[]> {
        return this.http
            .get(this._baseUrl + 'api/persons')
            .map(res => res.json());
    };

    addContact(contact: Contact): Observable<any> {
        const body = JSON.stringify(contact);
        return this.http
            .post(this._baseUrl + 'api/persons', body, {
                headers: this.headers
            })
            .map(res => res.json());
    };

    editContact(id: number, contact: Contact): Observable<any> {
        const body = JSON.stringify(contact);
        return this.http
            .put(this._baseUrl + `api/persons/${id}`, body, {
                headers: this.headers
            })
            .map(res => res.json());
    };

    deleteContact(id: number): Observable<any> {
        return this.http
            .delete(this._baseUrl + `api/persons/${id}`)
            .map(res => res.json());
    };

    getContacts(): Contact[] {
        return this.contacts;
    };

    setContacts(data: Contact[]) {
        this.contacts = data;
    };

    //getContact(id: number): Contact {
    //    return this.contacts.filter(c => {
    //         c.id === id
    //    });
    //}
}
