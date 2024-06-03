import React from 'react';
import FullCalendar from '@fullcalendar/react'
import dayGridPlugin from '@fullcalendar/daygrid' // a plugin!
import interactionPlugin from "@fullcalendar/interaction";
import { Tooltip } from "bootstrap";

let tooltipInstance: any = null;

export default function Calendar({events, eventClick}: any) {

    const handleMouseEnter = (info: any) => {
        if (info.event.extendedProps.description) {
            tooltipInstance = new Tooltip(info.el, {
                title: info.event.extendedProps.description,
                html: true,
                placement: "top",
                trigger: "hover",
                container: "body"
            });
    
            tooltipInstance.show();
        }
    };
    
    const handleMouseLeave = () => {
        if (tooltipInstance) {
            tooltipInstance.dispose();
            tooltipInstance = null;
        }
    };

    return (
        <FullCalendar
            plugins={[ dayGridPlugin, interactionPlugin ]}
            initialView="dayGridMonth"
            eventMouseEnter={handleMouseEnter}
            eventMouseLeave={handleMouseLeave}
            events={events}
            eventClick={(event) => {
                handleMouseLeave();
                eventClick(event);
            }}
        />
    )
}