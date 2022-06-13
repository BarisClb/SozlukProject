import { discussionTypes } from "../types/discussionTypes";
import { actionHelpers } from "../helpers/actionHelpers";

// GET DISCUSSION LIST

const getDiscussionList = (sortValues) => {
	return (dispatch) => {
		console.log("first");
		actionHelpers.getEntityHelper(
			"Discussions",
			sortValues,
			dispatch,
			discussionTypes.GetDiscussions
		);
	};
};

export const discussionActions = {
	getDiscussionList,
};
