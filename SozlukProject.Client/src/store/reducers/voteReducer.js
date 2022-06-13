import { voteTypes } from "../types/voteTypes";

export default function voteReducer(state = { votes: [], singleVote: {} }, action) {
	switch (action.type) {
		case voteTypes.GetVotes:
			return { ...state, votes: action.payload };

		case voteTypes.GetSingleVote:
			return { ...state, singleVote: action.payload };

		case voteTypes.CreateVote:
			return state;

		case voteTypes.UpdateVote:
			return state;

		case voteTypes.DeleteVote:
			return state;

		default:
			return state;
	}
}
