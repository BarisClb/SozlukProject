import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { discussionActions } from "../../store/actions/discussionActions";
import "./css/index.css";
import { Link } from "react-router-dom";

function MainPage() {
	const dispatch = useDispatch();

	const data = useSelector((state) => state.discussion.discussions);
	console.log(data);
	useEffect(() => {
		dispatch(discussionActions.getDiscussionList());
	}, []);

	let [message] = useState(
		"hello hellohellohello hellohellohellohellohe llohellohell ohellohellohellohello"
	);
	return (
		<div id="mainpage-content-wrapper" className="d-flex">
			<div id="mainpage-discussion-list" className="col-3 col-xs-0">
				<h3 id="discussion-lis-header">discussions</h3>
				<ul id="discussion-list">
					{data &&
						data.map((discussion, index) => (
							<li className="discussion-list-discussion" key={discussion?.id ?? index}>
								<Link
									to={`/${discussion?.id}`}
									className="discussion-list-discussion-title"
								>
									{discussion?.title?.length < 60
										? discussion?.title
										: `${discussion?.title.slice(0, 57)}...`}
									<small className="discussion-list-discussion-comment-count">
										{discussion?.commentCount}
									</small>
								</Link>
							</li>
						))}
					<li className="discussion-list-discussion">
						<Link to={"/"} className="discussion-list-discussion-title">
							{message.length < 60 ? message : `${message.slice(0, 57)}...`}
							<small className="discussion-list-discussion-comment-count">1000</small>
						</Link>
					</li>
				</ul>
			</div>
			<div id="mainpage-discussion" className="col">
				<h4>discussion list</h4>
				<ul>
					<li></li>
				</ul>
			</div>
		</div>
	);
}

export default MainPage;
