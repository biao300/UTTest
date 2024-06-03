import React, { useState } from 'react';

import TextAreaField from 'components/form/textarea';



export default function EventAdd({ submitClick }: any) {
    const [title, setTitle] = useState("");
    const [titleError, setTitleError] = useState("");
    const [titleTouched, setTitleTouched] = useState(false);

    const [description, setDescription] = useState("");
    const [descriptionError, setDescriptionError] = useState("");
    const [descriptionTouched, setDescriptionTouched] = useState(false);

    const [dateError, setDateError] = useState("");

    const [formMessage, setFormMessage] = useState("");

    const checkValidation = ({title, description, date}: any) => {
        let titleError = "";
        let descriptionError = "";
        let dateError = "";

        if (!title && title === "") {
            titleError = "Please input term";
        }

        if (!description && description === "") {
            descriptionError = "Please input definition";
        }

        if (!date && date === "") {
            dateError = "Please input event date";
        }

        setTitleTouched(true);
        setDescriptionTouched(true);
        setTitleError(titleError);
        setDescriptionError(descriptionError);
        setDateError(dateError);

        setFormMessage("");

        return titleError === "" && descriptionError === "" && dateError === "";
    }

    const handleSubmit = (event: any) => {
        event.preventDefault();


        const addItem = {
            title: event.target.title.value,
            description: event.target.description.value,
            date: event.target.date.value
        }

        const valid = checkValidation(addItem);

        if (valid) {
            submitClick(event);
        }
    }

    return (<form onSubmit={handleSubmit}>
        <h2>Add new event</h2>
        <TextAreaField 
            name="title"
            input={title}
            label="Title: "
            maxLength={20}
            rows={1}
            cols={20}
            touched={titleTouched}
            error={titleError}
            handleChange={(e: any) => setTitle(e.currentTarget.value)}
        />
        <TextAreaField 
            name="description"
            input={description}
            label="Description: "
            maxLength={200}
            rows={10}
            cols={20}
            touched={descriptionTouched}
            error={descriptionError}
            handleChange={(e: any) => setDescription(e.currentTarget.value)}
        />

        <input type="date" name="date" />
        <br/>
        <span className="error-msg">{dateError}</span>

        <p><input type="submit" value='Add Event' min={new Date().toISOString().split('T')[0]} /></p>
        <p>{formMessage}</p>
    </form>)
}