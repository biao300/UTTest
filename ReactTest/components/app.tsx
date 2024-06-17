import React, { useState } from 'react';

import Calendar from './calendar';
import EventAdd from './eventAdd';
import EventSearch from './eventSearch';

export default function App() {

    const [events, setEvents] = useState([]);
    const [filteredEvents, SetFilteredEvents] = useState([]);

    const addEvent = (event: any) => {
        const newEvents = [
            ...events,
            {
                title: event.target.title.value,
                start: event.target.date.value,
                end: event.target.date.value,
                allDay: true,
                editable: false,
                clickable: true,
                overlap: true,
                extendedProps: {
                    description: event.target.description.value
                }
            }
        ]

        setEvents(newEvents);
        SetFilteredEvents(newEvents);
    };

    const removeEvent = (e: any) => {
        console.log(e.event._def);
        const removeEvent = e.event._def;
        let result = events.filter((item: any) => {
            if (item.title !== removeEvent.title || 
                item.extendedProps.description !== removeEvent.extendedProps.description) {
                return true;
            }
            return false;
        });

        setEvents(result);
        SetFilteredEvents(result);
    }

    const searchEvent = (search: string) => {
        console.log(`search: `, search);

        let result = events.filter((item: any) => {
            if (item.title.includes(search) || item.extendedProps.description.includes(search)) {
                return true;
            }
            return false;
        });

        SetFilteredEvents(result);
    }

    return(<div className='container'>
        <div className='ui-left'>
            <EventAdd submitClick={addEvent} />
            <br/>
            <EventSearch searchClick={searchEvent}/>
        </div>
        <div className='calendar'>
            <Calendar events={filteredEvents} eventClick={removeEvent}/>
        </div>
    </div>)
}