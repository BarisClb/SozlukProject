import { commonTypes } from "../types/commonTypes";

export default function commonReducer(state = { IsLoading: false }, action) {
	switch (action.type) {
		case commonTypes.AsyncStarted:
			return {
				...state,
				IsLoading: action.payload ?? true,
			};

		case commonTypes.AsyncEnd:
			return {
				...state,
				IsLoading: false,
				Error: null,
			};

		case commonTypes.AsyncError:
			return {
				...state,
				IsLoading: false,
				Error: action.payload.Error,
			};

		case commonTypes.ClearError:
			return {
				...state,
				Error: null,
				GlobalError: null,
			};

		case commonTypes.GlobalError:
			return {
				...state,
				IsLoading: false,
				GlobalError: action.payload.Error,
			};

		default:
			return state;
	}
}
