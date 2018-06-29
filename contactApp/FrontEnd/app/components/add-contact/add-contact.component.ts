import { Component, OnInit } from '@angular/core';
import { ContactService } from '../../services/contact.service';
import { ActivatedRoute } from '@angular/router';
import { Contact } from '../../models/contact';

@Component({
    selector: 'add-contact',
    templateUrl: './add-contact.component.html',
    styleUrls: ['./add-contact.component.css']
})
export class AddContactComponent implements OnInit {
    contactObject: Contact = {
        id: 0,
        first_name: '',
        last_name: '',
        phone: ''
    };
    submitted: boolean = false;
    updated: boolean = false;
    errorOccured: boolean = false;
    isUpdate: boolean = false;
    errorMessage: string = '';
    constructor(private _contactService: ContactService, private _route: ActivatedRoute) {
    };

    ngOnInit() {
        this._route.params.subscribe(params => {
            const id = +params['id'];
            if (id) {
                this.isUpdate = true;
                this.getContact(id);
            }
            
        });
    };

    addContact(contact: Contact) {
        this._contactService.addContact(contact)
            .subscribe(result => {
                console.log(result);
                this.submitted = true;
            }, error => {
                error = error.json();
                console.error(error);
                this.errorOccured = true;
                this.errorMessage = error.message;
            });
    };

    editContact(contact: Contact) {
        this._contactService.editContact(contact.id, contact)
            .subscribe(result => {
                console.log(result);
                this.updated = true;
            }, error => {
                error = error.json();
                console.error(error);
                this.errorOccured = true;
                this.errorMessage = error.message;
            })
    };

    getContact(id: number) {
        this.contactObject = this._contactService.getContacts().find(c => c.id === id) as Contact;
    };
    
    submitContact() {
        if (this.isUpdate) {
            this.editContact(this.contactObject);
        } else {
            this.addContact(this.contactObject);
        }
    };

    

 
}

