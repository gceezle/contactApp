import { Component, OnInit, Input } from '@angular/core';
import { ContactService } from '../../services/contact.service';
import { Contact } from '../../models/contact';
import { Router } from '@angular/router';

@Component({
    selector: 'contacts',
    templateUrl: './contacts.component.html'
})
export class ContactComponent implements OnInit {
    public contacts: Contact[] = [];
    

    constructor(private _contactService: ContactService, private _router: Router) {
    }

    ngOnInit() {
        this.getContacts();
    };

    getContacts() {
        this._contactService.getAllContacts().subscribe(result => {
            this.contacts = result as Contact[];
            this._contactService.setContacts(this.contacts);
        }, error => console.error(error));
    };

    editContact(id: number) {
        this._router.navigate(['/edit', id]);
    };
    
    deleteContact(id: number, name: string) {
        if (confirm(`Are you sure you want to delete ${name}`)) {
            const index = this.contacts.findIndex(e => e.id === id);
            if (index !== -1) {
                this._contactService.deleteContact(id).subscribe(result => {
                    console.log(result);
                    this.contacts.splice(index, 1);
                }, error => console.log(error));
                
            }
            
            
        }
        
    };
}

