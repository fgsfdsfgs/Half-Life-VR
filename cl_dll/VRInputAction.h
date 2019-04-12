#pragma once

#include <unordered_map>
#include <string>

#include "openvr/openvr.h"

class VRInputAction
{
public:
	typedef void(*DigitalActionHandler)(const vr::InputDigitalActionData_t&, const std::string&);
	typedef void(*AnalogActionHandler)(const vr::InputAnalogActionData_t&, const std::string&);
	typedef void(*PoseActionHandler)(const vr::InputPoseActionData_t&, const std::string&);

	enum class ActionType
	{
		INVALID,
		DIGITAL,
		ANALOG,
		POSE
	};

	VRInputAction();
	VRInputAction(const std::string& id, vr::VRActionHandle_t handle, DigitalActionHandler handler);
	VRInputAction(const std::string& id, vr::VRActionHandle_t handle, AnalogActionHandler handler);
	VRInputAction(const std::string& id, vr::VRActionHandle_t handle, PoseActionHandler handler);

	void HandleInput();

private:
	void HandleDigitalInput();
	void HandleAnalogInput();
	void HandlePoseInput();

	std::string				m_id;
	vr::VRActionHandle_t	m_handle{ 0 };
	ActionType				m_type{ ActionType::INVALID };

	DigitalActionHandler	m_digitalActionHandler{ nullptr };
	AnalogActionHandler		m_analogActionHandler{ nullptr };
	PoseActionHandler		m_poseActionHandler{ nullptr };
};

