import { combineReducers } from "redux";
import commentReducer from "./commentReducer";
import commonReducer from "./commonReducer";
import discussionReducer from "./discussionReducer";
import userReducer from "./userReducer";
import voteReducer from "./voteReducer";

const createRootReducer = () =>
	combineReducers({
		comment: commentReducer,
		common: commonReducer,
		discussion: discussionReducer,
		user: userReducer,
		vote: voteReducer,
	});

export default createRootReducer;
