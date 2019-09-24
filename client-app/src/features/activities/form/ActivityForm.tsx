import React, { useState, FormEvent } from "react";
import { Segment, Form, Button } from "semantic-ui-react";
import { IActivity } from "../../../app/models/activity";
import { v4 as uuid } from "uuid";

interface IProps {
  setEditMode: (editMode: boolean) => void;
  activity: IActivity | null;
  createActivity: (activity: IActivity) => void;
  editActivity: (activity: IActivity) => void;
  submitting: boolean;
}

const ActivityForm: React.FC<IProps> = ({
  setEditMode, 
  activity: initialAct,
  createActivity,
  editActivity,
  submitting }) => {

  const initForm = () => {
    if(initialAct)
      return initialAct;
    else {
      return {
        id: "",
        title: "",
        category: "",
        description: "",
        date: "",
        city: "",
        venue: ""
      };
    }
  };

  const [activity, setActivity] = useState<IActivity>(initForm);

  const handleInputChange = (event: FormEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const {name, value} = event.currentTarget;
    setActivity({...activity, [name]: value});
  }

  const handleSubmit = () => {
    if(activity.id.length === 0) {
      let newActivity = {
        ...activity,
        id: uuid()
      };
      createActivity(newActivity);
    }else {
        editActivity(activity);
    }
  }


  return (
    <Segment clearing>
      <Form onSubmit={handleSubmit} >
        <Form.Input name="title" onChange={handleInputChange} value={activity.title} placeholder= "Title"/>
        <Form.TextArea rows={2} name="description" onChange={handleInputChange} value={activity.description} placeholder= "Description"/>
        <Form.Input name="category" onChange={handleInputChange} value={activity.category} placeholder= "Category"/>
        <Form.Input name="date" onChange={handleInputChange} value={activity.date} type="datetime-local" placeholder= "Date"/>
        <Form.Input name="city" onChange={handleInputChange} value={activity.city} placeholder= "City"/>
        <Form.Input name="venue" onChange={handleInputChange} value={activity.venue} placeholder= "Venue"/>
        <Button loading={submitting} floated="right" positive type="submit" content="Submit" />
        <Button onClick={() => setEditMode(false)} floated="right" type="button" content="Cancel" />
    </Form>
    </Segment>
  );
};

export default ActivityForm;
