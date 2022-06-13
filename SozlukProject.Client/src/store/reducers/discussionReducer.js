import { discussionTypes } from "../types/discussionTypes";

export default function discussionReducer(
	state = { discussions: [], singleDiscussion: {} },
	action
) {
	switch (action.type) {
		case discussionTypes.GetDiscussions:
			return { ...state, discussions: action.payload };

		case discussionTypes.GetSingleDiscussion:
			return { ...state, singleDiscussion: action.payload };

		case discussionTypes.CreateDiscussion:
			return state;

		case discussionTypes.UpdateDiscussion:
			return state;

		case discussionTypes.DeleteDiscussion:
			return state;

		default:
			return state;
	}
}
