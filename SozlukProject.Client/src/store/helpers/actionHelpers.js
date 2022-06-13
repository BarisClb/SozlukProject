import { apiHelpers } from "./apiHelpers";

const apiUrl = process.env.REACT_APP_URL;

// GET ENTITY LIST

const getEntityHelper = async (entityName, sortValues, dispatchAction, dispatchType) => {
	console.log("second");
	// Creating the base url, I add 'Reversed' as a default here because the first parameter takes '?' at the start, while the rest takes '&'
	let url = `${apiUrl}/${entityName}?Reversed=${sortValues?.Reversed ?? "false"}`;
	// Adding the rest of the Parameters, if there are any. We have default values in API so we actually don't need to specify any sortValues here, unless we want a specific list
	if (sortValues?.SearchWord) url += `&SearchWord=${sortValues?.SearchWord}`;
	if (sortValues?.PageNumber) url += `&PageNumber=${sortValues?.PageNumber}`;
	if (sortValues?.PageSize) url += `&PageSize=${sortValues?.PageSize}`;
	if (sortValues?.OrderBy) url += `&OrderBy=${sortValues?.OrderBy}`;

	apiHelpers.getRequest(url, dispatchAction, dispatchType);
};

// GET ENTITY BY ID

const getEntityByIdHelper = async (entityName, entityId) => {};

// GET *ENTITY LIST* BY *ENTITY*

const getEntitiesByEntityHelper = async (
	manyEntityName,
	singleEntityName,
	singleEntityId,
	sortValues
) => {};

// CREATE ENTITY

const createEntityHelper = async (entityName, entityCreateDto) => {};

// UPDATE ENTITY

const updateEntityHelper = async (entityName, entityUpdateDto) => {};

// DELETE ENTITY

const deleteEntityHelper = async (entityName, entityId) => {};

export const actionHelpers = {
	getEntityHelper,
	getEntityByIdHelper,
	getEntitiesByEntityHelper,
	createEntityHelper,
	updateEntityHelper,
	deleteEntityHelper,
};
