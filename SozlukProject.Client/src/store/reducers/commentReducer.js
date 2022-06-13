import { commentTypes } from "../types/commentTypes";

export default function commentReducer(state = { comments: [], singleComment: {} }, action) {
	switch (action.type) {
		case commentTypes.GetComments:
			return { ...state, comments: action.payload };

		case commentTypes.GetSingleComment:
			return { ...state, singleComment: action.payload };

		case commentTypes.CreateComment:
			return state;

		case commentTypes.UpdateComment:
			return state;

		case commentTypes.DeleteComment:
			return state;

		default:
			return state;
	}
}
